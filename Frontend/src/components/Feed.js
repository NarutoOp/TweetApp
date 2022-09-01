import React from "react";
import { Box } from "@mui/material";
import TweetBox from "./TweetBox";
import Post from "./Post";
import "./Feed.css";

// import FlipMove from "react-flip-move";

function Feed() {
  //   const [posts, setPosts] = useState([]);

  //   useEffect(() => {

  //       setPosts = ({
  //         displayName: "Rafeh Qazi",
  //         username: "cleverqazi",
  //         verified: true,
  //         text: tweetMessage,
  //         image: tweetImage,
  //         avatar:
  //           "https://kajabi-storefronts-production.global.ssl.fastly.net/kajabi-storefronts-production/themes/284832/settings_images/rLlCifhXRJiT0RoN2FjK_Logo_roundbackground_black.png",
  //       })
  //       , []);

  return (
    <Box className="feed" sx={{ width: { md: 1 / 3, xs: 1 } }}>
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
      <Post
        key="1"
        displayName="Arpit"
        username="Naruto"
        verified="true"
        text="text"
        avatar="avatar"
        image="https://media3.giphy.com/media/65ATdpi3clAdjomZ39/giphy.gif"
      />

      <Post
        key="1"
        displayName="Arpit"
        username="Naruto"
        verified="true"
        text="{post.text}"
        avatar="{post.avatar}"
        image="{post.image}"
      />
      <Post
        key="1"
        displayName="Arpit"
        username="Naruto"
        verified="true"
        text="{post.text}"
        avatar="{post.avatar}"
        image="{post.image}"
      />
    </Box>
  );
}

export default Feed;
