import {Button, Modal, Table} from "antd";
import React from "react";
import type {ColumnsType} from 'antd/es/table';
import './ConfigsTable.css'
import formatDate from "../utils/dateTimeFormat";
import {ExclamationCircleFilled, InfoCircleFilled} from "@ant-design/icons";
import EditConfigValueModal from "./EditConfigValueModal";

const testConfigs: Config[] = [
    new Date(),
    new Date(2023, 2, 18, 16, 50),
    new Date(2023, 2, 18, 20, 25),
    new Date(2023, 2, 20, 16, 25),
    new Date(2023, 5, 18, 16, 25),
    new Date(2025, 2, 18, 16, 25),
].map(x => ({name: "config-name", updated: x}))

interface Config {
    name: string;
    updated: Date;
}

const showInfo = () => {
    Modal.info({
        title: 'Open item',
        icon: <InfoCircleFilled/>,
        content: 'Some descriptions',
        onOk() {
            console.log('OK');
        }
    });
};

const showConfirm = () => {
    Modal.confirm({
        title: 'Do you want to delete this item?',
        icon: <ExclamationCircleFilled/>,
        content: 'Some descriptions',
        onOk() {
            console.log('OK');
        },
        onCancel() {
            console.log('Cancel');
        },
    });
};

function ConfigsTable() {
    const columns: ColumnsType<Config> = [
        {
            title: 'Config name',
            dataIndex: 'name',
            key: 'name',
            render: name => (<a className="configNameLink" onClick={showInfo}>{name}</a>)
        },
        {
            title: 'Last updated',
            dataIndex: 'updated',
            key: 'updated',
            align: "right",
            render: formatDate
        },
        {
            title: "Action",
            key: "action",
            align: "right",
            render: () => <Button danger onClick={showConfirm} className="ignore-parent-click">Delete</Button>,
            width: '10%'
        }
    ];

    return (
        <>
            <EditConfigValueModal open={true} onClose={() => {
            }} config={{zone: "default", key: "config-name"}}/>
            <Table
                className="configsTable"
                columns={columns}
                dataSource={testConfigs}
                size="middle"
                pagination={false}
            />
        </>
    )
}

export default ConfigsTable;