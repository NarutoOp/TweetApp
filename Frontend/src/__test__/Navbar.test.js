import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "../Auth/useAuth";
import Navbar from "../components/Navbar";

describe("Navbar Component Test", () => {
  render(
    <BrowserRouter>
      <AuthProvider>
        <Navbar />
      </AuthProvider>
    </BrowserRouter>
  );

  test("Render the Navbar with 3 buttons", () => {
    const ele = screen.getAllByRole("button");
    expect(ele).toHaveLength(3);
  });
});
