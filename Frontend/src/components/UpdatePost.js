import React, { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import { Box, IconButton } from "@mui/material";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { Update, ClearSharp } from "@mui/icons-material";
import Avatar from "../Utility/BackgroundLetterAvatars";
import KeyStore, { getUsername, getUserToken } from "../KeyStore";
import axios from "axios";

export default function Reply(props) {
  const [open, setOpen] = useState(false);
  const [message, setMessage] = useState("");

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const updateTweet = async (e) => {
    if (message !== "") {
      e.preventDefault();
      await axios
        .put(
          `${KeyStore.BaseURL}/${getUsername()}/update/${props.tweetId}`,
          {
            tweetMessage: {
              message: message,
            },
          },
          {
            headers: {
              Authorization: `Bearer ${getUserToken()}`,
            },
          }
        )
        .then((response) => {
          console.log(response);
        })
        .catch((e) => {
          console.log(e);
        });
      setMessage("");
      handleClose();

      props.stateChange();
    }
  };

  useEffect(() => {
    setMessage(props.message);
  }, []);

  return (
    <Box>
      <Button onClick={handleClickOpen}>
        <Update fontSize="small" sx={{ color: "#76ff03" }} />
        {props.replyCount}
      </Button>

      <Dialog open={open} onClose={handleClose} fullWidth={true}>
        <DialogTitle>
          <Box sx={{ display: "flex", justifyContent: "space-between" }}>
            <span>Update Tweet</span>
            <IconButton color="error" onClick={handleClose}>
              <ClearSharp />
            </IconButton>
          </Box>
        </DialogTitle>

        <DialogContent>
          <div className="tweetBox">
            <form>
              <div className="tweetBox__input">
                <Avatar name={getUsername()} />
                <textarea
                  required
                  onChange={(e) => setMessage(e.target.value)}
                  value={message}
                  type="text"
                />
              </div>

              <Button
                onClick={updateTweet}
                type="submit"
                className="tweetBox__tweetButton"
              >
                Update
              </Button>
            </form>
          </div>
        </DialogContent>
      </Dialog>
    </Box>
  );
}
