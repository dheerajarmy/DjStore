import { createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../components/home/HomePage";
import Catalog from "../../components/catalog/Catalog";
import ProductDetails from "../../components/catalog/ProductDetails";
import AboutUs from "../../components/about/AboutUs";
import ContactUs from "../../components/contact/ContactUs";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "catalog", element: <Catalog /> },
      { path: "catalog/:id", element: <ProductDetails /> },
      { path: "about", element: <AboutUs /> },
      { path: "contact", element: <ContactUs /> },
    ],
  },
]);
