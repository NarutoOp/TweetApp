import React, { useState, useEffect } from "react";
import { Box, IconButton, InputBase, Paper } from "@mui/material";
import { Search } from "@mui/icons-material";
import UserCard from "././UserCard";
import "../CSS/Feed.css";
import KeyStore from "../KeyStore";
import axios from "axios";

function Users() {
  const [users, setUsers] = useState([]);

  const [searchKey, setSearchKey] = useState("");
  const searchUser = async (e) => {
    e.preventDefault();
    await axios
      .get(`${KeyStore.BaseURL}/user/search/${searchKey}`)
      .then((response) => {
        console.log(response);
        setUsers(() => response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  };

  const fetchUsers = async () => {
    await axios
      .get(`${KeyStore.BaseURL}/users/all`)
      .then((response) => {
        console.log(response);
        setUsers(response.data.map((data) => data));
      })
      .catch((e) => {
        console.log(e);
      });
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  return (
    <Box className="feed" sx={{ width: { md: 2 / 3, xs: 1 } }}>
      <div className="feed__header">
        <h2>All Users</h2>
      </div>
      <form>
        <Paper
          component="form"
          sx={{
            p: "2px 4px",
            display: "flex",
            alignItems: "center",
            width: 1,
          }}
        >
          <InputBase
            sx={{ ml: 1, flex: 1 }}
            value={searchKey}
            onChange={(e) => setSearchKey(e.target.value)}
            placeholder="Search Users"
            inputProps={{ "aria-label": "search google maps" }}
          />
          <IconButton
            onClick={searchUser}
            type="button"
            sx={{ pr: "10px", mr: "40px" }}
            aria-label="search"
          >
            <Search />
          </IconButton>
        </Paper>
      </form>

      {users?.map((user) => (
        <UserCard
          key={user.id}
          displayName={user.firstName + " " + user.lastName}
          username={user.loginId}
          contact={user.contactNumber}
          email={user.email}
        />
      ))}
    </Box>
  );
}

export default Users;
