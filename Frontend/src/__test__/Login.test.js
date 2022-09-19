import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "../Auth/useAuth";
import Login from "../components/Login";

describe("Login Component Test", () => {
  render(
    <BrowserRouter>
      <AuthProvider>
        <Login />
      </AuthProvider>
    </BrowserRouter>
  );

  test("Render the login form with 2 buttons", () => {
    const ele = screen.getAllByRole("button");
    expect(ele).toHaveLength(2);
  });
});
