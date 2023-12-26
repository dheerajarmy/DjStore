import { useState, useEffect } from "react";
import { Products } from "../../app/models/Products";
import ProductList from "./ProductList";

export default function Catalog() {
  const [products, setproducts] = useState<Products[]>([]);

  useEffect(() => {
    fetch("http://localhost:5000/api/Products")
      .then((response) => response.json())
      .then((data) => setproducts(data));
  }, []);

  return (
    <>
      <ProductList products={products} />
    </>
  );
}
