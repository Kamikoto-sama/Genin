import {Button, Modal, Table} from "antd";
import React, {useState} from "react";
import type {ColumnsType} from 'antd/es/table';
import './ConfigsTable.css'
import formatDate from "../utils/dateTimeFormat";
import {ExclamationCircleFilled} from "@ant-design/icons";
import EditConfigValueModal from "./EditConfigValueModal";

const testConfigs: Config[] = [
    new Date(),
    new Date(2023, 2, 18, 16, 50),
    new Date(2023, 2, 18, 20, 25),
    new Date(2023, 2, 20, 16, 25),
    new Date(2023, 5, 18, 16, 25),
    new Date(2025, 2, 18, 16, 25),
].map((x, i) => ({name: "config-name" + i, updated: x}))

const testJsonValue = JSON.stringify({
    position: {
        x: 10,
        y: 20
    },
    valid: true,
    topology: [
        {
            name: "index1",
            address: "http://localhost"
        }
    ]
}, null, 2);


interface Config {
    name: string;
    updated: Date;
}

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

function buildColumns(showEdit: (configName: string) => void) {
    const columns: ColumnsType<Config> = [
        {
            title: 'Config name',
            dataIndex: 'name',
            key: 'name',
            render: name => (<a className="configNameLink" onClick={() => showEdit(name)}>{name}</a>)
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

    return columns;
}

function ConfigsTable() {
    const [configName, setConfigName] = useState<string | null>()
    const columns = buildColumns(setConfigName)

    return (
        <>
            <EditConfigValueModal
                open={configName != null}
                onClose={() => setConfigName(null)}
                config={{zone: "default", key: configName ?? '', value: testJsonValue}}
            />
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