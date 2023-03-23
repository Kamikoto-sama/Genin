import {Button, Dropdown, Table} from "antd";
import React from "react";
import type {ColumnsType} from 'antd/es/table';
import './ConfigsTable.css'
import formatDate from "../utils/dateTimeFormat";
import {ArrowLeftOutlined, MoreOutlined} from "@ant-design/icons";
import {Config} from "../utils/apiClient";

function buildColumns(onConfigSelect: (config: Config) => any, onBackClick: () => any) {
    const columns: ColumnsType<Config> = [
        {
            title: 'Config name',
            dataIndex: 'name',
            render: (name, config) => {
                if (config.isEmpty())
                    return (<Button type="text" onClick={onBackClick}><ArrowLeftOutlined/></Button>);
                return (<a className="configNameLink" onClick={() => onConfigSelect(config)}>{name}</a>);
            }
        },
        {
            title: 'Last updated',
            dataIndex: 'updated',
            key: 'updated',
            align: "right",
            render: (date, config) => config.isEmpty() ? <></> : formatDate(date)
        },
        {
            title: "Action",
            align: "right",
            render: (_, config) => {
                if (config.isEmpty())
                    return <></>
                return (
                    <Dropdown trigger={["click"]}>
                        <Button type="text"><MoreOutlined/></Button>
                    </Dropdown>
                );
            },
            width: '10%'
        }
    ];

    return columns;
}

function ConfigsTable({path, configs, onConfigSelect, onBackClick}: Props) {
    const columns = buildColumns(onConfigSelect, onBackClick);
    if (path.length > 0)
        configs = [Config.Empty, ...configs];

    return (
            <Table
                rowKey={(x) => x.name}
                className="configsTable"
                columns={columns}
                dataSource={configs}
                size="middle"
                pagination={{
                    position: ["topRight", "bottomRight"],
                    hideOnSinglePage: true,
                    defaultPageSize: 10,
                    showSizeChanger: true
                }}
            />
    )
}

interface Props {
    path: string[];
    configs: Config[];
    onConfigSelect: (config: Config) => any;
    onBackClick: () => any;
}

export default ConfigsTable;