import MainLayout from "../components/layouts/main/MainLayout";
import Sidebar from "../components/layouts/sidebar/Sidebar";
import Content from "../components/layouts/content/Content";
import DoughnutChart from "../components/charts/doughnut/DoughnutChart";
import { NextPageWithLayout } from "./page";
import { IChartData } from "../components/charts/models/ChartModels";

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
      label: "Proteins",
      data: 40,
      backgroundColor: "rgb(252, 180, 0)",
    },
  ];

  const caloriesCount = {
    status: "text-yellow-400",
    current: 2000,
    goal: 3000,
  };

  return (
    <section>
      <h2 className="text-2xl text-center uppercase tracking-widest mb-5">
        Current status
      </h2>
      <DoughnutChart data={data} />
      <h3 className="text-xl uppercase tracking-widest mt-7 text-center">
        Calories: {caloriesCount.current}/{caloriesCount.goal}
      </h3>
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
