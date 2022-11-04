import { useEffect, useState } from "react";
import { MealProduct } from "../../model/Meal";

export interface IProductList {
  productList: MealProduct[];
  updateProduct: (product: MealProduct, amount: number) => void;
}

export interface IInpuntOnClick {
  defaultValue?: number;
  name?: string;
  onBlur?: (value: number) => void;
}

const InpuntOnClick = ({ name, defaultValue, onBlur }: IInpuntOnClick) => {
  const [value, setValue] = useState(defaultValue);
  const [inputShow, setInputShow] = useState(false);

  const handleShowInput = () => {
    setInputShow(true);
  };

  const handleOnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const numberValue = parseFloat(e.target.value);
    setValue(numberValue);
  };

  const handleOnBlur = (e: React.ChangeEvent<HTMLInputElement>) => {
    const numberValue = parseFloat(e.target.value);
    setValue(numberValue);

    if (onBlur) {
      onBlur(numberValue);
    }

    setInputShow(false);
  };

  return inputShow ? (
    <input
      className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
      type="number"
      name={name}
      value={value}
      onChange={handleOnChange}
      onBlur={handleOnBlur}
    />
  ) : (
    <div onClick={handleShowInput}>{value}</div>
  );
};

const defaultSummary = {
  kcal: 0,
  proteins: 0,
  carbohydrates: 0,
  fats: 0,
};

const ProductList = ({ productList, updateProduct }: IProductList) => {
  const [summary, setSummary] = useState(defaultSummary);

  useEffect(() => {
    const newSummary =
      productList && productList.length
        ? productList
            .map((c) => ({
              kcal: c.kcal,
              proteins: c.proteins,
              carbohydrates: c.carbohydrates,
              fats: c.fats,
            }))
            .reduce((prev, next) => ({
              kcal: prev.kcal + next.kcal,
              proteins: prev.proteins + next.proteins,
              carbohydrates: prev.carbohydrates + next.carbohydrates,
              fats: prev.fats + next.fats,
            }))
        : defaultSummary;

    setSummary(newSummary);
  }, [productList]);

  return (
    <table className="table-auto">
      <thead>
        <tr className="border-slate-300 border-2">
          <td className="p-1.5">Amount</td>
          <td className="p-1.5">Name</td>
          <td className="p-1.5">Fats</td>
          <td className="p-1.5">Carbs</td>
          <td className="p-1.5">Proteins</td>
          <td className="p-1.5">Calories</td>
        </tr>
      </thead>
      <tbody>
        {productList.map((p) => (
          <tr key={p.id} className="border-slate-300 border-2">
            <td className="p-1.5">
              <InpuntOnClick
                defaultValue={p.amount}
                name="amount"
                onBlur={(amount) => updateProduct(p, amount)}
              />
            </td>
            <td className="p-1.5">
              <strong key={p.id}>{p.name}</strong>
            </td>
            <td className="p-1.5">
              <span>{p.fats}</span>
            </td>
            <td className="p-1.5">
              <span>{p.carbohydrates}</span>
            </td>
            <td className="p-1.5">
              <span>{p.proteins}</span>
            </td>
            <td className="p-1.5">
              <span>{p.kcal}</span>
            </td>
          </tr>
        ))}
        <tr className="border-slate-300 border-2">
          <td className="p-1.5"></td>
          <td className="p-1.5"></td>
          <td className="p-1.5">{summary.fats}</td>
          <td className="p-1.5">{summary.carbohydrates}</td>
          <td className="p-1.5">{summary.proteins}</td>
          <td className="p-1.5">{summary.kcal}</td>
        </tr>
      </tbody>
    </table>
  );
};

export default ProductList;
