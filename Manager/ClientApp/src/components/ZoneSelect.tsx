import React, {useEffect, useState} from "react";
import {Button, Divider, Select, Space} from "antd";
import {PlusOutlined} from "@ant-design/icons";
import {ApiClient} from "../utils/apiClient";

function ZoneSelect({onChange, onCreateZone}: Props) {
    const [zones, setZones] = useState<string[]>([]);

    useEffect(() => {
        ApiClient.getTopZones().then(x => setZones(x));
    })

    const options = zones && zones.map(x => ({value: x, label: x})) || []
    return (
        <Select
            style={{minWidth: '150px'}}
            showSearch
            placeholder="Zone name"
            className="zone-select"
            options={options}
            onChange={onChange}
            allowClear={true}
            dropdownRender={(menu) => (
                <>
                    {menu}
                    {
                        onCreateZone &&
                        <>
                            <Divider style={{margin: '8px 0'}}/>
                            <Space style={{padding: '0 8px 4px'}}>
                                <Button onClick={onCreateZone}>
                                    <PlusOutlined/> Create zone
								</Button>
							</Space>
						</>
                    }
                </>
            )}
        />
    )
}

interface Props {
    onCreateZone?: () => void;
    onChange?: (zone: string) => void;
}

interface State {
    zone?: string;
    zones?: string[];
}

export default ZoneSelect;