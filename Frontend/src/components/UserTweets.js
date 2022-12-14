import React, { useState, useEffect } from "react";
import { Box, IconButton, CircularProgress } from "@mui/material";
import Post from "./Post";
import "../CSS/Feed.css";
import KeyStore from "../KeyStore";
import axios from "axios";
import FlipMove from "react-flip-move";
import { useLocation, Link } from "react-router-dom";
import { useAuth } from "../Auth/useAuth";
import { ArrowBack } from "@mui/icons-material";

const UserTweets = () => {
  const { user } = useAuth();
  const location = useLocation();
  const [userTweets, setUserTweets] = useState([]);
  const [tweetState, setTweetState] = useState(false);

  const ChangeTweetState = () => {
    setTweetState(!tweetState);
  };

  const fetchTweets = async () => {
    await axios
      .get(`${KeyStore.BaseURL}/${location.state.username}`, {
        headers: {
          Authorization: `Bearer ${user.userToken}`,
        },
      })
      .then((response) => {
        setUserTweets(response.data.map((data) => data));
      })
      .catch((e) => {
        console.log(e);
      });
  };

  useEffect(() => {
    fetchTweets();
  }, []);

  return (
    <Box className="feed" sx={{ width: { md: 2 / 3, xs: 1 } }}>
      <div className="feed__header">
        <IconButton>
          <Link to="/Users">
            <ArrowBack />
          </Link>
        </IconButton>
        <h2>User Tweets</h2>
      </div>

      {userTweets.length > 0 ? (
        <FlipMove>
          {userTweets?.map((tweet) => (
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
};

export default UserTweets;
