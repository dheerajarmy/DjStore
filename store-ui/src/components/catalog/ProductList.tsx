import { Grid, List } from "@mui/material";
import { Products } from "../../app/models/Products";
import ProductCard from "./ProductCard";
interface Props {
  products: Products[];
}
export default function ProductList({ products }: Props) {
  return (
    <>
      <Grid container spacing={4}>
        {products.map((product) => (
          <Grid item xs={3} key={product.id}>
            <ProductCard product={product} />
          </Grid>
        ))}
      </Grid>
    </>
  );
}
