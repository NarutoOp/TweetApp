import React, { useState } from "react";
import "./TweetBox.css";
import { Button } from "@mui/material";
import Avatar from "../Utility/BackgroundLetterAvatars";
import KeyStore, { getUsername, getUserToken } from "../KeyStore";
import axios from "axios";

function TweetBox(props) {
  const [tweetMessage, setTweetMessage] = useState("");
  const reply = {
    message: tweetMessage,
  };

  const tweet = {
    tweetMessage: {
      message: tweetMessage,
    },
  };

  const sendTweet = async (e) => {
    if (tweetMessage !== "") {
      e.preventDefault();
      let payload = tweet;
      let url = `${KeyStore.BaseURL}/${getUsername()}/add`;
      if (props.isReply) {
        payload = reply;
        url = `${KeyStore.BaseURL}/${getUsername()}/reply/${props.tweetId}`;
      }
      await axios
        .post(url, payload, {
          headers: {
            Authorization: `Bearer ${getUserToken()}`,
          },
        })
        .then((response) => {
          console.log(response);
        })
        .catch((e) => {
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
      <form>
        <div className="tweetBox__input">
          <Avatar name={getUsername()} />
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
    </div>
  );
}

export default TweetBox;
