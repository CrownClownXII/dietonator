import Content from "../components/layouts/content/Content";
import MainLayout from "../components/layouts/main/MainLayout";
import Sidebar from "../components/layouts/sidebar/Sidebar";
import { MealTypeEnum } from "../model/Meal";
import {
  useAddMealMutation,
  useGetMealsQuery,
} from "../store/slices/meal/mealSlice";
import { NextPageWithLayout } from "./page";

const Plans: NextPageWithLayout = () => {
  const { data, isFetching, isLoading } = useGetMealsQuery();
  const [
    addMeal, // This is the mutation trigger
    { isLoading: isAdding }, // This is the destructured mutation result
  ] = useAddMealMutation();

  const submitMeal = () => {
    addMeal({ name: "TestMeal", type: MealTypeEnum.calculableMeal });
  };

  return (
    <section>
      <h2 className="text-2xl text-center uppercase tracking-widest mb-5">
        Meal plans
      </h2>
      {isFetching ? (
        data?.map((c, i) => <div>{i}</div>)
      ) : (
        <strong>Fetching</strong>
      )}
      <button disabled={isAdding} onClick={submitMeal}>
        Add meal
      </button>
    </section>
  );
};

export default Plans;

Plans.getLayout = (page) => {
  return (
    <MainLayout>
      <>
        <Sidebar />
        <Content>{page}</Content>
      </>
    </MainLayout>
  );
};
