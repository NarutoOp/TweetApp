import React, { forwardRef } from "react";
import "./Post.css";
import Avatar from "../Utility/BackgroundLetterAvatars";
import Reply from "./Reply";
import Like from "./Like";

const postTime = (time) => {
  let now = new Date();
  let current = new Date(time);

  let response;
  if (now.getFullYear() - current.getFullYear() !== 0) {
    response =
      current.getDate() +
      " " +
      current.toLocaleString("en-US", { month: "short" }) +
      ", " +
      current.getFullYear();
  } else if (
    now.getMonth() - current.getMonth() !== 0 &&
    now.getDate() - current.getDate() !== 0
  ) {
    response =
      current.getDate() +
      " " +
      current.toLocaleString("en-US", { month: "short" });
  } else if (now.getHours() - current.getHours() !== 0) {
    response = now.getHours() - current.getHours() + "h";
  } else if (now.getMinutes() - current.getMinutes() !== 0) {
    response = now.getMinutes() - current.getMinutes() + "m";
  } else {
    response = now.getSeconds() - current.getSeconds() + "s";
  }
  return response;
};
const Post = forwardRef((props, ref) => {
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
              <Like tweetId={props.id} like={props.like} />
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
