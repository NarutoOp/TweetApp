import React, { useState, useEffect } from "react";
import { Box } from "@mui/material";
import TweetBox from "./TweetBox";
import Post from "./Post";
import "./Feed.css";
import KeyStore from "../KeyStore";
import axios from "axios";
import FlipMove from "react-flip-move";
import { useAuth } from "../Auth/useAuth";

function Feed() {
  const { user } = useAuth();
  const [tweets, setTweets] = useState([]);
  const [tweetState, setTweetState] = useState(false);
  const ChangeTweetState = () => {
    setTweetState(!tweetState);
  };

  const fetchTweets = async () => {
    await axios
      .get(`${KeyStore.BaseURL}/all`, {
        headers: {
          Authorization: `Bearer ${user.userToken}`,
        },
      })
      .then((response) => {
        setTweets(response.data.map((data) => data));
      })
      .catch((e) => {
        console.log(e);
      });
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
        {tweets?.map((tweet) => (
          <Post
            key={tweet.id}
            id={tweet.id}
            displayName={tweet.tweetMessage.name}
            username={tweet.tweetMessage.username}
            created={tweet.tweetMessage.created}
            message={tweet.tweetMessage.message}
            reply={tweet.reply}
            replyCount={tweet.reply?.length}
            like={tweet.like}
            stateChange={ChangeTweetState}
          />
        ))}
      </FlipMove>
    </Box>
  );
}

export default Feed;
