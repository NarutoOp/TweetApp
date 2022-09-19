import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "../Auth/useAuth";
import ForgotPassword from "../components/ForgotPassword";

describe("ForgotPassword Component Test", () => {
  render(
    <BrowserRouter>
      <AuthProvider>
        <ForgotPassword />
      </AuthProvider>
    </BrowserRouter>
  );

  test("Render the ForgotPassword form with 1 buttons", () => {
    const ele = screen.getAllByRole("button");
    expect(ele).toHaveLength(1);
  });
});
