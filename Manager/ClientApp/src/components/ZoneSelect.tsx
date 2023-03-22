import React from "react";
import {Button, Divider, Select, Space} from "antd";
import {PlusOutlined} from "@ant-design/icons";
import {ApiClient} from "../utils/apiClient";

class ZoneSelect extends React.Component<Props, State> {
    constructor(props: Props) {
        super(props);

        this.state = {
            zones: []
        }
    }

    async componentDidMount() {
        const zones = await ApiClient.getTopZones();
        this.setState({zones});
    }

    render() {
        const {onCreateZone, onSelect} = this.props;
        const {zones} = this.state;

        const options = zones && zones.map(x => ({value: x, label: x})) || []
        return (
            <Select
                style={{minWidth: '150px'}}
                showSearch
                placeholder="Zone name"
                className="zone-select"
                options={options}
                onChange={onSelect}
                dropdownRender={(menu) => (
                    <>
                        {menu}
                        <Divider style={{margin: '8px 0'}}/>
                        <Space style={{padding: '0 8px 4px'}}>
                            <Button onClick={onCreateZone}>
                                <PlusOutlined/> Create zone
                            </Button>
                        </Space>
                    </>
                )}
            />
        )
    }
}

interface Props {
    onCreateZone?: () => void;
    onSelect?: (zone: string) => void;
}

interface State {
    zone?: string;
    zones?: string[];
}

export default ZoneSelect;