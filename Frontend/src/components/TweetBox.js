import React, { useState } from "react";
import "./TweetBox.css";
import { Button } from "@mui/material";
import Avatar from "../Utility/BackgroundLetterAvatars";
import KeyStore, { getUsername, getUserToken } from "../KeyStore";
import axios from "axios";

function TweetBox() {
  const [tweetMessage, setTweetMessage] = useState("");
  const [tweetImage, setTweetImage] = useState("");

  const sendTweet = (e) => {
    e.preventDefault();
    axios
      .post(
        `${KeyStore.BaseURL}/${getUsername()}/add`,
        {
          tweetMessage: {
            message: tweetMessage,
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

    setTweetMessage("");
    setTweetImage("");
  };

  return (
    <div className="tweetBox">
      <form>
        <div className="tweetBox__input">
          <Avatar name={getUsername()} />
          <input
            onChange={(e) => setTweetMessage(e.target.value)}
            value={tweetMessage}
            placeholder="What's happening?"
            type="text"
          />
        </div>
        <input
          value={tweetImage}
          onChange={(e) => setTweetImage(e.target.value)}
          className="tweetBox__imageInput"
          placeholder="Optional: Enter image URL"
          type="text"
        />

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
