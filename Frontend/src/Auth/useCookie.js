import { useState } from "react";
import Cookies from "js-cookie";

export const useCookie = (keyName, defaultValue) => {
  const [storedValue, setStoredValue] = useState(() => {
    try {
      const value = Cookies.get(keyName);
      if (value) {
        return JSON.parse(value);
      } else {
        Cookies.set(keyName, JSON.stringify(defaultValue), { expires: 1 });
        return defaultValue;
      }
    } catch (err) {
      return defaultValue;
    }
  });
  const setValue = (newValue) => {
    try {
      Cookies.set(keyName, JSON.stringify(newValue));
    } catch (err) {}
    setStoredValue(newValue);
  };
  return [storedValue, setValue];
};
