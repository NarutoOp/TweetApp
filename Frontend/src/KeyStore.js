import Cookies from "js-cookie";

export const getUserToken = () => Cookies.get("user_token");
export const getUsername = () => Cookies.get("username");

const KeyStore = {
  BaseURL: "https://localhost:44333/api/v1.0/tweets",
};

export default KeyStore;
