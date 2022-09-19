import React, { useState, useEffect } from "react";
import { Box, CircularProgress } from "@mui/material";
import TweetBox from "./TweetBox";
import Post from "./Post";
import "../CSS/Feed.css";
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

      {tweets.length > 0 ? (
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
      ) : (
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
      )}
    </Box>
  );
}

export default Feed;
