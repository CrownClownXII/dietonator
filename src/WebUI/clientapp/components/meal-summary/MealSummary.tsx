import { useEffect, useState } from "react";
import { CaloricInfo } from "../../model/UserStatistic";
import DoughnutChart from "../charts/doughnut/DoughnutChart";
import { IChartData } from "../charts/models/ChartModels";

export interface IMealSummary {
  caloricInfo: CaloricInfo;
  label: string;
  onClick?: () => void;
  className: string;
}

const MealSummary = ({
  caloricInfo,
  label,
  className,
  onClick,
}: IMealSummary): JSX.Element => {
  const [chartData, setChartData] = useState<IChartData[] | null>(null);

  useEffect(() => {
    const data: IChartData[] = [
      {
        label: "Fats",
        data: caloricInfo.fats,
        backgroundColor: "rgb(255, 0, 55)",
      },
      {
        label: "Proteins",
        data: caloricInfo.proteins,
        backgroundColor: "rgb(0, 153, 255)",
      },
      {
        label: "Carbohydrates",
        data: caloricInfo.carbohydrates,
        backgroundColor: "rgb(252, 180, 0)",
      },
    ];

    setChartData(data);
  }, [caloricInfo]);

  const handleOnClick = () => {
    if(onClick) {
      onClick();
    }
  }

  return (
    <article className={className}>
      <h2
        className="text-2xl text-center uppercase tracking-widest mb-5"
        onClick={handleOnClick}
      >
        {label}
      </h2>
      {chartData && <DoughnutChart data={chartData} />}
      <h3 className="text-xl uppercase tracking-widest mt-7 text-center">
        Calories: {caloricInfo.kcal}/3000
      </h3>
    </article>
  );
};

export default MealSummary;
