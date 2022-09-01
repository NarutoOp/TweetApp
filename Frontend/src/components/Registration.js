import { React } from "react";
import {
  Typography,
  Button,
  Stack,
  TextField,
  Paper,
  Grid,
} from "@mui/material";
import "./Registration.css";

const Registration = () => {
  return (
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
        <form>
          <Typography variant="h4" sx={{ mb: 2 }} color="deepskyblue">
            Registration
          </Typography>
          <Grid container spacing={2}>
            <Grid item xs={6}>
              <TextField
                required
                id="outlined-required"
                label="First Name"
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                required
                id="outlined-required"
                label="Last Name"
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                id="outlined-required"
                label="Email"
                type="email"
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                required
                id="outlined-required"
                label="Contact Number"
                type="tel"
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                required
                id="outlined-required"
                label="Username"
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                id="outlined-password-input"
                label="Password"
                type="password"
                autoComplete="current-password"
                fullWidth="true"
              />
            </Grid>
            <Grid item xs={6}>
              <TextField
                id="outlined-password-input"
                label="Confirm Password"
                type="password"
                autoComplete="current-password"
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
            Log in
          </Button>
        </form>
      </Paper>
    </Stack>
  );
};

export default Registration;
