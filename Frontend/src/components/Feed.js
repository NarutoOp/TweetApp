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
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    axios
      .get(`${KeyStore.BaseURL}/all`, {
        headers: {
          Authorization: `Bearer ${getUserToken()}`,
        },
      })
      .then((response) => {
        console.log(response);
        setPosts(() => response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  }, []);

  return (
    <Box className="feed" sx={{ width: { md: 2 / 3, xs: 1 } }}>
      <div className="feed__header">
        <h2>Home</h2>
      </div>

      <TweetBox />

      {/* <FlipMove>
        <Post
          key="1"
          displayName="Arpit"
          username="Naruto"
          verified="true"
          text="{post.text}"
          avatar="{post.avatar}"
          image="{post.image}"
        />
      </FlipMove> */}
      <FlipMove>
        {posts.map((post) => (
          <Post
            key={post.id}
            displayName="Test User"
            username={post.tweetMessage.username}
            verified="true"
            text={post.tweetMessage.message}
            image="https://media3.giphy.com/media/65ATdpi3clAdjomZ39/giphy.gif"
          />
        ))}
      </FlipMove>
    </Box>
  );
}

export default Feed;
