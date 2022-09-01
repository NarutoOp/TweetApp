import { React } from "react";
import { Typography, Button, Stack, TextField, Paper } from "@mui/material";
import "./Login.css";

const Login = () => {
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
        <form>
          <Stack direction="column" alignItems="center" spacing={3}>
            <Typography variant="h4" color="deepskyblue">
              Login
            </Typography>
            <TextField
              required
              id="outlined-required"
              label="Username"
              fullWidth="true"
            />

            <TextField
              id="outlined-password-input"
              label="Password"
              type="password"
              autoComplete="current-password"
              fullWidth="true"
            />
            <Button
              alignItems="left"
              variant="contained"
              color="primary"
              fullWidth="true"
            >
              Log in
            </Button>
          </Stack>
          <Button sx={{ mt: 2 }} color="primary">
            <h6>Forgot Password</h6>
          </Button>
        </form>
      </Paper>
    </Stack>
  );
};

export default Login;
