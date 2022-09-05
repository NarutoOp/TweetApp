import { React, useState } from "react";
import {
  Alert,
  Typography,
  Button,
  Stack,
  TextField,
  Paper,
  Snackbar,
} from "@mui/material";
import { useForm } from "react-hook-form";
import axios from "axios";
import KeyStore from "../KeyStore";
import "./Login.css";
import { useNavigate } from "react-router-dom";

const ForgotPassword = () => {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const { register, handleSubmit, reset } = useForm();
  const [errors, setErrors] = useState({});

  const onSubmit = async (e) => {
    setErrors({});
    await axios
      .put(`${KeyStore.BaseURL}/${e.userName}/forgot`, e.password.toString(), {
        headers: {
          "Content-Type": "application/json",
        },
      })
      .then((response) => {
        reset();
        alert("Password changed successfully !!");
        navigate("/login");
      })
      .catch((err) => {
        setErrors(err.response.data);
        console.log(err);
        setOpen(true);
      });
  };

  return (
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
              Forgot Password
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
              Change Password
            </Button>
          </Stack>
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

export default ForgotPassword;