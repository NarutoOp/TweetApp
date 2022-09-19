import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "../Auth/useAuth";
import Registration from "../components/Registration";

describe("Registration Component Test", () => {
  render(
    <BrowserRouter>
      <AuthProvider>
        <Registration />
      </AuthProvider>
    </BrowserRouter>
  );

  test("Render the Registration form with 1 buttons", () => {
    const ele = screen.getAllByRole("button");
    expect(ele).toHaveLength(1);
  });
});
