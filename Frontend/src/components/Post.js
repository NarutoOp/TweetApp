import React, { forwardRef } from "react";
import "./Post.css";
import Avatar from "../Utility/BackgroundLetterAvatars";
import {
  VerifiedUser,
  ChatBubbleOutline,
  Repeat,
  FavoriteBorder,
} from "@mui/icons-material";

const Post = forwardRef(
  ({ displayName, username, verified, text, image }, ref) => {
    return (
      <div className="post" ref={ref}>
        <div className="post__avatar">
          <Avatar name={username} />
        </div>
        <div className="post__body">
          <div className="post__header">
            <div className="post__headerText">
              <h3>
                {displayName}{" "}
                <span className="post__headerSpecial">
                  {verified && <VerifiedUser className="post__badge" />} @
                  {username}
                </span>
              </h3>
            </div>
            <div className="post__headerDescription">
              <p>{text}</p>
            </div>
          </div>

          {image ? <img src={image} alt="" /> : null}
          <div className="post__footer">
            <ChatBubbleOutline fontSize="small" />
            <Repeat fontSize="small" />
            <FavoriteBorder fontSize="small" />
          </div>
        </div>
      </div>
    );
  }
);

export default Post;
