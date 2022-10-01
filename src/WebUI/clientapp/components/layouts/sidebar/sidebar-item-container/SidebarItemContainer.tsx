import Link from "next/link";
import { useMemo, useState } from "react";
import SidebarItem, { ISidebarItem } from "../sidebar-item/SidebarItem";
import Separator from "../../../separators/Separator";
import { useSelector } from "react-redux";
import { RootState } from "../../../../store/store";

export interface ISidebarItemContainer {
  items: ISidebarItem[];
}

const SidebarItemContainer = ({
  items,
}: ISidebarItemContainer): JSX.Element => (
  <ul className="space-y-2 mt-5">
    <li>
      {items.map((c) => (
        <SidebarItem key={c.href} href={c.href} label={c.label} />
      ))}
    </li>
  </ul>
);

export default SidebarItemContainer;
