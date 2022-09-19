import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "../Auth/useAuth";
import DeleteTweet from "../components/DeleteTweet";

describe("DeleteTweet Component Test", () => {
  render(
    <BrowserRouter>
      <AuthProvider>
        <DeleteTweet />
      </AuthProvider>
    </BrowserRouter>
  );

  test("Render the DeleteTweet with 1 buttons", () => {
    const ele = screen.getAllByRole("button");
    expect(ele).toHaveLength(1);
  });
});
