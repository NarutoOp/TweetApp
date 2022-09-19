import { React, useState } from "react";
import {
  Alert,
  Typography,
  Button,
  Stack,
  TextField,
  Paper,
  Snackbar,
  CircularProgress,
  Box,
} from "@mui/material";
import { useForm } from "react-hook-form";
import axios from "axios";
import KeyStore from "../KeyStore";
import "../CSS/Login.css";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../Auth/useAuth";

const Login = () => {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const { register, handleSubmit, reset } = useForm();
  const [errors, setErrors] = useState({});
  let [loading, setLoading] = useState(false);
  const { login } = useAuth();

  const onSubmit = async (e) => {
    setLoading(true);
    setErrors({});
    await axios
      .post(`${KeyStore.BaseURL}/login`, e)
      .then((response) => {
        setLoading(false);
        login({
          fullName: response.data.name,
          userToken: response.data.token,
          userName: response.data.userName,
        });
        reset();
      })
      .catch((e) => {
        setErrors(e.response.data);
        setLoading(false);
        console.log(e);
        setOpen(true);
      });
  };

  return loading ? (
    <Box
      sx={{
        display: "flex",
        height: 1,
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <CircularProgress color="secondary" />
    </Box>
  ) : (
    <Stack
      className="Login"
      direction="column"
      justifyContent="center"
      alignItems="center"
    >
      <Paper
        className="loginPaper"
        sx={{ width: { md: 1 / 3 } }}
        elevation={13}
      >
        <form onSubmit={handleSubmit(onSubmit)}>
          <Stack direction="column" alignItems="center" spacing={3}>
            <Typography variant="h4" color="deepskyblue">
              Login
            </Typography>
            <TextField
              required
              label="Username"
              {...register("userName")}
              fullWidth="true"
            />

            <TextField
              label="Password"
              type="password"
              {...register("password")}
              fullWidth="true"
            />
            <Button
              type="submit"
              alignItems="left"
              variant="contained"
              color="primary"
              fullWidth="true"
            >
              Log in
            </Button>
          </Stack>
          <Button
            onClick={() => navigate("/ForgotPassword")}
            sx={{ mt: 2 }}
            color="primary"
          >
            <h6>Forgot Password</h6>
          </Button>
        </form>
        {errors != null ? (
          <Snackbar
            anchorOrigin={{
              vertical: "top",
              horizontal: "center",
            }}
            open={open}
            autoHideDuration={20000}
            onClose={() => setOpen(false)}
          >
            <Alert
              elevation={6}
              variant="filled"
              onClose={() => setOpen(false)}
              severity="error"
              sx={{ width: "100%" }}
            >
              {errors?.Info?.length > 0
                ? errors.Info.map((err, a) => (
                    <div>{a++ + ". " + err.Message}</div>
                  ))
                : errors.Message}
            </Alert>
          </Snackbar>
        ) : null}
      </Paper>
    </Stack>
  );
};

export default Login;
