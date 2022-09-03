import React, { useState, useEffect } from "react";
import { Box } from "@mui/material";
import TweetBox from "./TweetBox";
import Post from "./Post";
import "./Feed.css";
import KeyStore, { getUserToken } from "../KeyStore";
import axios from "axios";
import FlipMove from "react-flip-move";

// import FlipMove from "react-flip-move";

function Feed() {
  const [tweets, setTweets] = useState([]);
  const [tweetState, setTweetState] = useState(false);
  const ChangeTweetState = () => {
    setTweetState(!tweetState);
  };

  const fetchTweets = async () => {
    await axios
      .get(`${KeyStore.BaseURL}/all`, {
        headers: {
          Authorization: `Bearer ${getUserToken()}`,
        },
      })
      .then((response) => {
        console.log(response);
        setTweets(response.data.map((data) => data));
      })
      .catch((e) => {});
  };

  useEffect(() => {
    fetchTweets();
  }, [tweetState]);

  return (
    <Box className="feed" sx={{ width: { md: 2 / 3, xs: 1 } }}>
      <div className="feed__header">
        <h2>Home</h2>
      </div>

      <TweetBox stateChange={ChangeTweetState} />

      <FlipMove>
        {tweets.map((tweet) => (
          <Post
            key={tweet.tweetMessage.message}
            displayName="Test User"
            username={tweet.tweetMessage.username}
            verified="true"
            text={tweet.tweetMessage.message}
            image="https://media3.giphy.com/media/65ATdpi3clAdjomZ39/giphy.gif"
          />
        ))}
      </FlipMove>
    </Box>
  );
}

export default Feed;
