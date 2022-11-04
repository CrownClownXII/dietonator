import { ChangeEvent } from "react";
import { Product } from "../../../model/Product";
import { useGetProductsQuery } from "../../../store/slices/meal/mealSlice";

export interface IProductSelector {
  onSelect: (product: Product) => void;
}

const ProductSelector = ({ onSelect }: IProductSelector) => {
  const { data, isFetching } = useGetProductsQuery({
    searchBy: null,
  });

  const onChange = (e: ChangeEvent<HTMLSelectElement>) => {
    const product = data?.find((c) => c.id === e.target.value);

    if (!product) {
      return;
    }

    onSelect(product);
  };

  return (
    <>
      {!isFetching && data && (
        <select onChange={onChange}>
          {data.map((c) => (
            <option key={c.id} value={c.id}>
              {c.name} [F {c.fats} | P {c.proteins} | C {c.carbohydrates} | K {c.kcal}]
            </option>
          ))}
        </select>
      )}
    </>
  );
};

export default ProductSelector;
