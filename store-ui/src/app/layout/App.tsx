import { useState } from "react";
import Catalog from "../../components/catalog/Catalog";
import Header from "./Header";
import "react-toastify/dist/ReactToastify.css";
import {
  Container,
  CssBaseline,
  ThemeProvider,
  createTheme,
} from "@mui/material";
import { Outlet } from "react-router-dom";
import { ToastContainer } from "react-toastify";

function App() {
  const [darkMode, setDarkmode] = useState(false);
  const paletType = darkMode ? "dark" : "light";
  const theme = createTheme({
    palette: {
      mode: paletType,
      background: {
        default: paletType === "light" ? "#eaeaea" : "#121212",
      },
    },
  });

  function handleThemeChange() {
    setDarkmode(!darkMode);
  }
  return (
    <ThemeProvider theme={theme}>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
      <CssBaseline />
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
      <Container>
        <Outlet />
      </Container>
    </ThemeProvider>
  );
}

export default App;
