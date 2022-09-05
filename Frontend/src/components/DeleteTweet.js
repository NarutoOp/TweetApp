import React, { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import { Box, IconButton } from "@mui/material";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { DeleteForever, ClearSharp } from "@mui/icons-material";
import Avatar from "../Utility/BackgroundLetterAvatars";
import KeyStore from "../KeyStore";
import axios from "axios";
import { useAuth } from "../Auth/useAuth";

export default function DeleteTweet(props) {
  const { user } = useAuth();
  const [open, setOpen] = useState(false);
  const [message, setMessage] = useState("");

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const deleteTweet = async () => {
    await axios
      .delete(`${KeyStore.BaseURL}/${user.userName}/delete/${props.tweetId}`, {
        headers: {
          Authorization: `Bearer ${user.userToken}`,
        },
      })
      .then((response) => {
        console.log(response);
      })
      .catch((e) => {
        console.log(e);
      });
    setMessage("");
    handleClose();

    props.stateChange();
  };

  useEffect(() => {
    setMessage(props.message);
  }, []);

  return (
    <Box>
      <Button onClick={handleClickOpen}>
        <DeleteForever fontSize="small" color="error" />
      </Button>

      <Dialog open={open} onClose={handleClose} fullWidth={true}>
        <DialogTitle>
          <Box sx={{ display: "flex", justifyContent: "space-between" }}>
            <span>Are you Sure ?</span>
            <IconButton color="error" onClick={handleClose}>
              <ClearSharp />
            </IconButton>
          </Box>
        </DialogTitle>

        <DialogContent>
          <Button onClick={deleteTweet} color="error">
            Delete
          </Button>
          <Button onClick={handleClose}>No</Button>
        </DialogContent>
      </Dialog>
    </Box>
  );
}
