import { IChartData } from "../components/charts/models/ChartModels";
import Content from "../components/layouts/content/Content";
import MainLayout from "../components/layouts/main/MainLayout";
import Sidebar from "../components/layouts/sidebar/Sidebar";
import MealSummary from "../components/meal-summary/MealSummary";
import { NextPageWithLayout } from "./page";

const Home: NextPageWithLayout = () => {
  const data: IChartData[] = [
    {
      label: "Fats",
      data: 20,
      backgroundColor: "rgb(255, 0, 55)",
    },
    {
      label: "Proteins",
      data: 40,
      backgroundColor: "rgb(0, 153, 255)",
    },
    {
      label: "Carbohydrates",
      data: 40,
      backgroundColor: "rgb(252, 180, 0)",
    },
  ];

  const caloriesCount = {
    status: "text-yellow-400",
    current: 2000,
    goal: 3000,
  };

  const test = [1, 2, 3, 4, 5, 6, 7];

  return (
    <section className="flex flex-wrap">

    </section>
  );
};

export default Home;

Home.getLayout = (page) => {
  return (
    <MainLayout>
      <>
        <Sidebar />
        <Content>{page}</Content>
      </>
    </MainLayout>
  );
};
