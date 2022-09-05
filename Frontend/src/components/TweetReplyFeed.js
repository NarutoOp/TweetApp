import React, { useState, useEffect } from "react";
import { Box } from "@mui/material";
import Post from "./Post";
import "./Feed.css";
import FlipMove from "react-flip-move";

function TweetReplyFeed(props) {
  const [replies, setReplies] = useState([]);
  useEffect(() => {
    setReplies(() => props.reply);
  }, []);
  return (
    <Box className="feed">
      <div className="feed__header">
        <h2>Tweet Replies</h2>
      </div>

      <FlipMove>
        {replies
          .slice(0)
          .reverse()
          .map((tweet) => (
            <Post
              displayName="Test User"
              username={tweet.username}
              created={tweet.created}
              message={tweet.message}
              isReply="true"
            />
          ))}
      </FlipMove>
    </Box>
  );
}

export default TweetReplyFeed;
