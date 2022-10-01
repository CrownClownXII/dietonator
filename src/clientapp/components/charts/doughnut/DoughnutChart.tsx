import { useMemo } from "react";
import { Doughnut } from "react-chartjs-2";
import Legend from "./legend/Legend";
import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  ChartData,
} from "chart.js";
import { IChartData, mapToDoughnutModel } from "../models/ChartModels";

ChartJS.register(ArcElement, Tooltip);

export interface IDoughnutChart {
  data: IChartData[];
}

const DoughnutChart = ({ data }: IDoughnutChart): JSX.Element => {
  const options = useMemo(
    () => ({
      responsive: true,
      plugins: {
        legend: {
          display: false,
        },
      },
    }),
    []
  );

  const chartModel: ChartData<"doughnut", number[], string> =
    mapToDoughnutModel(data);

  return (
    <div>
      <Doughnut data={chartModel} options={options} />
      <Legend data={data} />
    </div>
  );
};

export default DoughnutChart;
