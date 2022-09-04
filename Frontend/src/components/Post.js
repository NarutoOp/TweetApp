import React, { forwardRef } from "react";
import "./Post.css";
import Avatar from "../Utility/BackgroundLetterAvatars";
import { Button } from "@mui/material";
import { FavoriteBorder } from "@mui/icons-material";
import Reply from "./Reply";

const Post = forwardRef((props, ref) => {
  const postTime = (time) => {
    let current = new Date(time);
    let cDate =
      current.getFullYear() +
      "-" +
      (current.getMonth() + 1) +
      "-" +
      current.getDate();
    let cTime =
      current.getHours() +
      ":" +
      current.getMinutes() +
      ":" +
      current.getSeconds();
    return cDate + "  " + cTime;
  };
  return (
    <div className="post" ref={ref}>
      <div className="post__avatar">
        <Avatar name={props.username} />
      </div>
      <div className="post__body">
        <div className="post__header">
          <div className="post__headerText">
            <h3>
              {props.displayName}{" "}
              <span className="post__headerSpecial">@{props.username}</span>
              <span className="show_time">{postTime(props.created)}</span>
            </h3>
          </div>
          <div className="post__headerDescription">
            <p>{props.message}</p>
          </div>
        </div>
        {props.isReply ? null : (
          <div>
            <div className="post__footer">
              <Button color="error">
                <FavoriteBorder fontSize="small" />
                {props.likeCount}
              </Button>
              <Reply
                replyCount={props.replyCount}
                tweetId={props.id}
                stateChange={props.stateChange}
                reply={props.reply}
              />
            </div>
          </div>
        )}
      </div>
    </div>
  );
});

export default Post;
