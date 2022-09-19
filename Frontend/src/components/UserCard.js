import React from "react";
import "../CSS/Post.css";
import Avatar from "../Utility/BackgroundLetterAvatars";
import { Button } from "@mui/material";
import { Link } from "react-router-dom";

const UserCard = (props) => {
  return (
    <div className="post">
      <div className="post__avatar">
        <Avatar name={props.displayName} />
      </div>
      <div className="post__body">
        <div className="post__header">
          <div className="post__headerText">
            <h3>
              {props.displayName}{" "}
              <span className="post__headerSpecial">@{props.username}</span>
              <span className="show_time">{}</span>
            </h3>
          </div>
          <div className="post__headerDescription">
            <ul>
              <li>Email : {props.email}</li>
              <li>Contact : {props.contact}</li>
            </ul>
          </div>
        </div>
        <div>
          <div className="post__footer">
            <Button>
              <Link to={`/UserTweets`} state={{ username: props.username }}>
                Show User Tweets
              </Link>
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserCard;
