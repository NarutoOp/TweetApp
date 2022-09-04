import * as React from "react";
import Button from "@mui/material/Button";
import { Box, IconButton } from "@mui/material";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { ChatBubbleOutline, ClearSharp } from "@mui/icons-material";
import TweetBox from "./TweetBox";
import TweetReplyFeed from "./TweetReplyFeed";

export default function Reply(props) {
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Button onClick={handleClickOpen}>
        <ChatBubbleOutline fontSize="small" />
        {props.replyCount}
      </Button>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>
          <Box sx={{ display: "flex", justifyContent: "space-between" }}>
            <span>Reply</span>
            <IconButton color="error" onClick={handleClose}>
              <ClearSharp />
            </IconButton>
          </Box>
        </DialogTitle>
        <DialogContent>
          <div className="feed">
            <TweetBox
              isReply="true"
              tweetId={props.tweetId}
              handleClose={handleClose}
              stateChange={props.stateChange}
            />
            {props.reply !== null ? (
              <TweetReplyFeed reply={props.reply} />
            ) : null}
          </div>
        </DialogContent>
      </Dialog>
    </div>
  );
}
