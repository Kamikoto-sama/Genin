import React from "react";
import {Button, Divider, Select, Space} from "antd";
import {PlusOutlined} from "@ant-design/icons";

const testZones = ["default", "prod", "staging"];

function ZoneSelect({onCreateZone}: { onCreateZone: () => void }) {
    const options = testZones.map(x => ({value: x, label: x}))

    function createZone(event: React.MouseEvent) {
        event.preventDefault()
        onCreateZone()
    }

    return (
        <Select
            style={{minWidth: '150px'}}
            showSearch
            placeholder="Zone name"
            className="zone-select"
            options={options}
            dropdownRender={(menu) => (
                <>
                    {menu}
                    <Divider style={{margin: '8px 0'}}/>
                    <Space style={{padding: '0 8px 4px'}}>
                        <Button onClick={createZone}>
                            <PlusOutlined/> Create zone
                        </Button>
                    </Space>
                </>
            )}
        />
    )
}

export default ZoneSelect;