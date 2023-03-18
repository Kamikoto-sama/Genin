import {Breadcrumb} from "antd";
import React from "react";

const testRoute = ["default", "app", "settings", "configs"]

function ConfigRoute() {
    return (
        <Breadcrumb>
            {testRoute.map(part => (<Breadcrumb.Item>{part}</Breadcrumb.Item>))}
        </Breadcrumb>
    )
}

export default ConfigRoute;