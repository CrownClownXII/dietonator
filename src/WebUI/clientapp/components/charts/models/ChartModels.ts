import { ChartData } from "chart.js";

export interface IChartData {
  label: string;
  data: number;
  backgroundColor: string;
}

export function mapToDoughnutModel(
  dataToTransform: IChartData[]
): ChartData<"doughnut", number[], string> {
  const labels: string[] = dataToTransform.map((c) => c.label);
  const data: number[] = dataToTransform.map((c) => c.data);
  const backgroundColor: string[] = dataToTransform.map(
    (c) => c.backgroundColor
  );

  return {
    labels,
    datasets: [
      {
        data,
        backgroundColor,
      },
    ],
  };
}
