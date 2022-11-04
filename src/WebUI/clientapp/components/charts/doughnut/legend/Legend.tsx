import { IChartData } from "../../models/ChartModels";

export interface IDoughnutChart {
  data: IChartData[];
}

const Legend = ({ data }: IDoughnutChart): JSX.Element => {
  return (
    <div className="flex w-full justify-between p-1 mt-2">
      {data.map((c) => (
        <article key={c.backgroundColor} className="flex items-center p-1">
          <span>{c.label}</span>
          <span
            className="w-4 h-4 ml-2 mt-0.5 border-white border"
            style={{ backgroundColor: c.backgroundColor }}
          ></span>
        </article>
      ))}
    </div>
  );
};

export default Legend;
