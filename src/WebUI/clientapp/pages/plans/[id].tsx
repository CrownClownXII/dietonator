import { useRouter } from "next/router";
import ButtonPrimary from "../../components/buttons/buttonPrimary/ButtonPrimary";
import Content from "../../components/layouts/content/Content";
import MainLayout from "../../components/layouts/main/MainLayout";
import Sidebar from "../../components/layouts/sidebar/Sidebar";
import MealSummary from "../../components/meal-summary/MealSummary";
import ProductList from "../../components/product-list/ProductList";
import ProductSelector from "../../components/selectors/product-selector/ProductSelector";
import { MealProduct, MealTypeEnum } from "../../model/Meal";
import {
  useAddMealMutation,
  useAddMealProductMutation,
  useGetDailyMealPlanQuery,
  useUpdateMealProductMutation,
} from "../../store/slices/meal/mealSlice";
import { NextPageWithLayout } from "../page";

const Plan: NextPageWithLayout = () => {
  const { query } = useRouter();

  const { data, isFetching } = useGetDailyMealPlanQuery({
    date: query.id as string,
  });

  const [addMeal] = useAddMealMutation();
  const [addMealProduct] = useAddMealProductMutation();
  const [updateMeal] = useUpdateMealProductMutation();

  const handleAddMeal = async () => {
    await addMeal({
      name: "New Meal",
      type: MealTypeEnum.calculableMeal,
      forDate: data?.forDate,
    });
  };

  const handleAddProduct = async (mealId: string, productId: string) => {
    await addMealProduct({
      mealId,
      productId,
      amount: 100,
    });
  };

  const handleUpdateProduct = async (
    mealProduct: MealProduct,
    amount: number
  ) => {
    await updateMeal({
      mealId: mealProduct.mealId,
      mealProductId: mealProduct.id,
      amount: amount,
    });
  };

  return (
    <section>
      <h2 className="text-2xl text-center uppercase tracking-widest mb-5">
        {data?.forDate}
      </h2>
      <div>{isFetching && "Fetching"}</div>
      {data && (
        <div>
          <MealSummary
            className="max-w-xs p-2.5 mb-2.5"
            label={data.forDate}
            caloricInfo={data.caloricInfo}
          />
          <div>
            <ButtonPrimary text="Add Meal" onClick={handleAddMeal} />
          </div>
          <div>
            {data.meals.map((c) => (
              <div className="p-4" key={c.id}>
                <ProductList
                  productList={c.products}
                  updateProduct={handleUpdateProduct}
                />
                <ProductSelector
                  onSelect={(p) => handleAddProduct(c.id, p.id)}
                />
              </div>
            ))}
          </div>
        </div>
      )}
    </section>
  );
};

export default Plan;

Plan.getLayout = (page) => {
  return (
    <MainLayout>
      <>
        <Sidebar />
        <Content>{page}</Content>
      </>
    </MainLayout>
  );
};
