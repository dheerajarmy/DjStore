import { useState, useEffect } from "react";
import { Products } from "../../app/models/Products";
import ProductList from "./ProductList";
import agent from "../../app/api/agent";
import Loading from "../../app/layout/Loading";
import { error } from "console";

export default function Catalog() {
  const [products, setproducts] = useState<Products[]>([]);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    agent.Catalog.list()
      .then((products) => setproducts(products))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <Loading message={"Loading Products..."} />;
  return (
    <>
      <ProductList products={products} />
    </>
  );
}
