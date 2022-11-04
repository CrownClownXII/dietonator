import { useRouter } from "next/router";
import { useState } from "react";
import Content from "../../components/layouts/content/Content";
import MainLayout from "../../components/layouts/main/MainLayout";
import Sidebar from "../../components/layouts/sidebar/Sidebar";
import MealSummary from "../../components/meal-summary/MealSummary";
import { formatDate } from "../../helpers/Date";
import { useGetMealPlansQuery } from "../../store/slices/meal/mealSlice";
import { NextPageWithLayout } from "../page";

const Plans: NextPageWithLayout = () => {
  const [range, setRange] = useState({
    dateFrom: formatDate(new Date()),
    dateTo: formatDate(new Date()),
  });

  const router = useRouter();

  const { data, isFetching } = useGetMealPlansQuery(range);

  const moveToPage = (date: string) => {
    router.push(`plans/${date.replaceAll("/", "-")}`);
  };

  return (
    <section>
      <h2 className="text-2xl text-center uppercase tracking-widest mb-5">
        Meal plans
      </h2>
      <div>{isFetching && "Fetching"}</div>
      {data &&
        data.map((c) => (
          <div key={c.forDate}>
            <MealSummary
              className="max-w-xs p-2.5 mb-2.5"
              label={c.forDate}
              caloricInfo={c.caloricInfo}
              onClick={() => moveToPage(c.forDate)}
            />
          </div>
        ))}
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
