import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Paper, Typography, IconButton, Divider } from '@material-ui/core';
import moment from 'moment';
import Style from './CardStyle';
import Edit from '@material-ui/icons/Edit';
import { Link } from 'react-router-dom';

export default function Card(props) {
  const { data, url } = props;
  const classes = Style();

  function handleOpen(id) {
    props.id(id);
  }
  return (
    <React.Fragment>
      <CssBaseline />
      <div className={classes.root}>
        <div style={{ display: 'flex', flexWrap: 'wrap' }}>
          {data &&
            data.map((data, index) => {
              const dateToFormat = data.createdAt;
              return (
                <div style={{ display: 'flex', flexWrap: 'wrap' }} key={index}>
                  <div className={classes.div}>
                    <div style={{ position: 'relative' }}>
                      <Paper className={classes.paper}>
                        <div
                          style={{
                            position: 'relative'
                          }}
                        >
                          <div className={classes.box}>
                            <Typography
                              style={{ left: 0, textTransform: 'uppercase' }}
                              color="primary"
                              gutterBottom
                            >
                              {data.name}
                            </Typography>
                            <Typography
                              variant="body2"
                              gutterBottom
                              className={classes.date}
                            >
                              {moment(dateToFormat).format('DD/MM/YYYY')}
                            </Typography>
                          </div>
                          <Divider />
                          <div
                            style={{ position: 'absolute', right: 0, top: 0 }}
                          >
                            <IconButton
                              component={url && Link}
                              to={url && `/admin/${url}/${data.name}`}
                              onClick={() => handleOpen(data.id)}
                            >
                              <Edit />
                            </IconButton>
                          </div>
                        </div>
                      </Paper>
                    </div>
                  </div>
                </div>
              );
            })}
        </div>
      </div>
    </React.Fragment>
  );
}
