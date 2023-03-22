import {Breadcrumb} from "antd";
import React from "react";

function ConfigRoute({zone, path = [], onClick}: Props) {
    const parts = [zone, ...path];
    return (
        <Breadcrumb>
            {
                parts.map((part, index) => (
                    <Breadcrumb.Item key={index} onClick={() => onClick(index)}>
                        {index == parts.length - 1 ? part : (<a>{part}</a>)}
                    </Breadcrumb.Item>
                ))
            }
        </Breadcrumb>
    )
}

interface Props {
    zone: string;
    path?: string[];
    onClick: (index: number) => any;
}

export default ConfigRoute;