import React, {useState} from 'react';
import {Col, ConfigProvider, Layout, Row, Space, theme} from 'antd';
import 'antd/dist/reset.css';
import ZoneSelect from "./components/ZoneSelect";
import ConfigsView from "./components/ConfigsView";
import CreateZoneModal from "./components/CreateZoneModal";

const App = () => {
    const [zone, setZone] = useState<string>();
    const [createZone, setCreateZone] = useState(true);

    return (
        <ConfigProvider theme={{algorithm: theme.darkAlgorithm}}>
            {createZone && <CreateZoneModal onClose={() => setCreateZone(false)}/>}
            <Layout className="bg-none">
                <Space size="large" direction="vertical">
                    <Layout.Header>
                        <ZoneSelect onSelect={setZone} onCreateZone={() => setCreateZone(true)}/>
                    </Layout.Header>
                    <Layout.Content>
                        <Row>
                            <Col offset={4} span={16}>
                                <ConfigsView zone={zone}/>
                            </Col>
                        </Row>
                    </Layout.Content>
                </Space>
            </Layout>
        </ConfigProvider>
    )
};

export default App;