import React, { useState } from "react";
import "../CSS/TweetBox.css";
import { Button, CircularProgress, Box } from "@mui/material";
import Avatar from "../Utility/BackgroundLetterAvatars";
import KeyStore from "../KeyStore";
import axios from "axios";
import { useAuth } from "../Auth/useAuth";

function TweetBox(props) {
  const { user } = useAuth();
  const [tweetMessage, setTweetMessage] = useState("");
  let [loading, setLoading] = useState(false);
  const reply = {
    name: user.fullName,
    message: tweetMessage,
  };

  const tweet = {
    tweetMessage: {
      name: user.fullName,
      message: tweetMessage,
    },
  };

  const sendTweet = async (e) => {
    setLoading(true);
    if (tweetMessage !== "") {
      e.preventDefault();
      let payload = tweet;
      let url = `${KeyStore.BaseURL}/${user.userName}/add`;
      if (props.isReply) {
        payload = reply;
        url = `${KeyStore.BaseURL}/${user.userName}/reply/${props.tweetId}`;
      }
      await axios
        .post(url, payload, {
          headers: {
            Authorization: `Bearer ${user.userToken}`,
          },
        })
        .then((response) => {
          setLoading(false);
        })
        .catch((e) => {
          setLoading(false);
          console.log(e);
        });
      setTweetMessage("");
      if (props.isReply) {
        props.handleClose();
      }
      props.stateChange();
    }
  };

  return (
    <div className="tweetBox">
      {loading ? (
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
        <form>
          <div className="tweetBox__input">
            <Avatar name={user.fullName} />
            <textarea
              required
              onChange={(e) => setTweetMessage(e.target.value)}
              value={tweetMessage}
              placeholder="What's happening?"
              type="text"
            />
          </div>

          <Button
            onClick={sendTweet}
            type="submit"
            className="tweetBox__tweetButton"
          >
            Tweet
          </Button>
        </form>
      )}
    </div>
  );
}

export default TweetBox;
