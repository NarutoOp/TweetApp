import React from "react";
import Navbar from "./Navbar";
import Container from "@mui/material/Container";
import "../CSS/Layout.css";

const Layout = (props) => {
  return (
    <div className="Layout">
      <Navbar />
      <Container className="Container">{props.children}</Container>
    </div>
  );
};

export default Layout;
