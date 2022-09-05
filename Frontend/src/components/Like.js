import React, { useState } from "react";
import Button from "@mui/material/Button";
import { Favorite, FavoriteBorder } from "@mui/icons-material";
import KeyStore, { getUserToken, getUsername } from "../KeyStore";
import axios from "axios";

export default function Like(props) {
  const [like, setLike] = useState({
    isLiked: props.like.some((ele) => ele === getUsername()),
    likeCount: props.like.length,
  });

  const handleLike = async () => {
    await axios
      .put(
        `${KeyStore.BaseURL}/${getUsername()}/like/${props.tweetId}`,
        {},
        {
          headers: {
            Authorization: `Bearer ${getUserToken()}`,
          },
        }
      )
      .then((response) => {
        setLike({
          likeCount: response.data.likeCount,
          isLiked: response.data.isLiked,
        });
      })
      .catch((e) => {
        console.log(e);
      });
  };

  return (
    <div>
      <Button color="error" onClick={handleLike}>
        {like.isLiked ? (
          <Favorite fontSize="small" />
        ) : (
          <FavoriteBorder fontSize="small" />
        )}
        {like?.likeCount}
      </Button>
    </div>
  );
}
