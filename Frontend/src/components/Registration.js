import { React, useState } from "react";
import { useForm } from "react-hook-form";
import {
  Typography,
  Button,
  Stack,
  TextField,
  Paper,
  Grid,
  Snackbar,
  Alert,
  CircularProgress,
  Box,
} from "@mui/material";
import "../CSS/Registration.css";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import KeyStore from "../KeyStore";

const Registration = () => {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const [openSuccess, setOpenSuccess] = useState(false);
  const [errors, setErrors] = useState({});
  const { register, handleSubmit, reset } = useForm();
  let [loading, setLoading] = useState(false);

  const handleClick = () => {
    setOpen(true);
  };

  const handleClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }

    setOpen(false);
  };

  const onSubmit = async (e) => {
    setLoading(true);
    setErrors({});
    await axios
      .post(`${KeyStore.BaseURL}/register`, e)
      .then((response) => {
        setLoading(false);
        setOpenSuccess(true);
      })
      .catch((e) => {
        setLoading(false);
        setErrors(e.response.data);
        handleClick();
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
      className="Registration"
      direction="column"
      justifyContent="center"
      alignItems="center"
    >
      <Paper
        className="registerPaper"
        sx={{ width: { md: 1 / 3 } }}
        elevation={13}
      >
        <form onSubmit={handleSubmit(onSubmit)}>
          <Typography variant="h4" sx={{ mb: 2 }} color="deepskyblue">
            Registration
          </Typography>
          <Grid container spacing={2}>
            <Grid item xs={6}>
              <TextField
                required
                label="First Name"
                {...register("firstName")}
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                required
                label="Last Name"
                {...register("lastName")}
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                label="Email"
                type="email"
                {...register("email")}
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                required
                label="Contact Number"
                type="tel"
                {...register("contactNumber")}
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                required
                label="Username"
                {...register("loginId")}
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                label="Password"
                type="password"
                {...register("password")}
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                label="Confirm Password"
                type="password"
                {...register("confirmPassword")}
                fullWidth="true"
              />
            </Grid>
          </Grid>
          <Button
            sx={{ mt: 2 }}
            type="submit"
            alignItems="left"
            variant="contained"
            color="primary"
            fullWidth="true"
          >
            Register
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
            onClose={handleClose}
          >
            <Alert
              elevation={6}
              variant="filled"
              onClose={handleClose}
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

        <Snackbar
          key="successSnackbar"
          anchorOrigin={{
            vertical: "top",
            horizontal: "center",
          }}
          open={openSuccess}
          autoHideDuration={6000}
          onClose={() => {
            setOpenSuccess(false);
            navigate("/login");
            reset();
          }}
        >
          <Alert
            elevation={6}
            variant="filled"
            severity="success"
            onClose={() => {
              setOpenSuccess(false);
              navigate("/login");
            }}
            sx={{ width: "100%" }}
          >
            Registered Succesfully
          </Alert>
        </Snackbar>
      </Paper>
    </Stack>
  );
};

export default Registration;
